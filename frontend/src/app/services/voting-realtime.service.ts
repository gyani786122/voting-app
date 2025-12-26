import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import * as signalR from '@microsoft/signalr';
import { Candidate } from '../models/candidate.model';
import { Voter } from '../models/voter.model';
import { CandidatesService } from './candidates.service';
import { VotersService } from './voters.service';
import { CastVoteResponse } from '../models/cast-vote-response.model';

@Injectable({
  providedIn: 'root'
})
export class VotingRealtimeService {
  private hubConnection: signalR.HubConnection | null = null;
  private readonly hubUrl = 'http://localhost:5000/hubs/voting';

  // BehaviorSubjects for state management
  private candidatesSubject = new BehaviorSubject<Candidate[]>([]);
  public candidates$ = this.candidatesSubject.asObservable();

  private votersSubject = new BehaviorSubject<Voter[]>([]);
  public voters$ = this.votersSubject.asObservable();

  // Subject for errors
  private errorSubject = new Subject<string>();
  public error$ = this.errorSubject.asObservable();

  constructor(
    private candidatesService: CandidatesService,
    private votersService: VotersService
  ) {}

  /**
   * Establishes connection to SignalR hub and loads initial data.
   */
  connect(): Observable<void> {
    return new Observable(observer => {
      // Load initial data
      Promise.all([
        this.candidatesService.getAllCandidates().toPromise(),
        this.votersService.getAllVoters().toPromise()
      ])
        .then(([candidates, voters]) => {
          if (candidates) this.candidatesSubject.next(candidates);
          if (voters) this.votersSubject.next(voters);

          // Establish SignalR connection
          this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(this.hubUrl, {
              skipNegotiation: true,
              transport: signalR.HttpTransportType.WebSockets
            })
            .withAutomaticReconnect([0, 0, 0, 5000, 5000, 10000])
            .build();

          // Register event handlers
          this.hubConnection.on('VoteCast', (response: CastVoteResponse) => {
            this.handleVoteCast(response);
          });

          this.hubConnection.onreconnected(() => {
            console.log('SignalR reconnected');
          });

          this.hubConnection.onreconnecting(() => {
            console.log('SignalR reconnecting...');
          });

          // Start connection
          this.hubConnection.start()
            .then(() => {
              console.log('SignalR connected');
              observer.next();
              observer.complete();
            })
            .catch(err => {
              console.error('SignalR connection error:', err);
              this.errorSubject.next('Failed to connect: ' + err);
              observer.error(err);
            });
        })
        .catch(err => {
          console.error('Failed to load initial data:', err);
          this.errorSubject.next('Failed to load data: ' + err);
          observer.error(err);
        });
    });
  }

  /**
   * Handles VoteCast event from SignalR hub.
   */
  private handleVoteCast(response: CastVoteResponse): void {
    // Update candidate
    const candidates = this.candidatesSubject.value;
    const candidateIndex = candidates.findIndex(c => c.id === response.candidate.id);
    if (candidateIndex !== -1) {
      candidates[candidateIndex] = response.candidate;
      this.candidatesSubject.next([...candidates]);
    }

    // Update voter
    const voters = this.votersSubject.value;
    const voterIndex = voters.findIndex(v => v.id === response.voter.id);
    if (voterIndex !== -1) {
      voters[voterIndex] = response.voter;
      this.votersSubject.next([...voters]);
    }
  }

  /**
   * Adds a new candidate to the local state.
   */
  addCandidate(candidate: Candidate): void {
    const candidates = this.candidatesSubject.value;
    this.candidatesSubject.next([...candidates, candidate]);
  }

  /**
   * Adds a new voter to the local state.
   */
  addVoter(voter: Voter): void {
    const voters = this.votersSubject.value;
    this.votersSubject.next([...voters, voter]);
  }

  /**
   * Disconnects from the SignalR hub.
   */
  disconnect(): Promise<void> {
    return this.hubConnection?.stop() ?? Promise.resolve();
  }

  /**
   * Gets current candidates state.
   */
  getCandidates(): Candidate[] {
    return this.candidatesSubject.value;
  }

  /**
   * Gets current voters state.
   */
  getVoters(): Voter[] {
    return this.votersSubject.value;
  }
}
