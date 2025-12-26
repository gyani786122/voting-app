import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CastVoteResponse } from '../models/cast-vote-response.model';

@Injectable({
  providedIn: 'root'
})
export class VotesService {
  private apiUrl = 'http://localhost:5000/api/votes';

  constructor(private http: HttpClient) {}

  /**
   * Casts a vote for the specified voter and candidate.
   * @param voterId The ID of the voter casting the vote.
   * @param candidateId The ID of the candidate receiving the vote.
   */
  castVote(voterId: string, candidateId: string): Observable<CastVoteResponse> {
    return this.http.post<CastVoteResponse>(this.apiUrl, {
      voterId,
      candidateId
    });
  }
}
