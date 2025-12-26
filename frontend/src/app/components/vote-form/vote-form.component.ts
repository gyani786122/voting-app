import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Candidate } from '../../models/candidate.model';
import { Voter } from '../../models/voter.model';
import { VotesService } from '../../services/votes.service';
import { VotingRealtimeService } from '../../services/voting-realtime.service';

@Component({
    selector: 'app-vote-form',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatSelectModule,
        MatButtonModule,
        MatSnackBarModule
    ],
    templateUrl: './vote-form.component.html',
    styleUrls: ['./vote-form.component.css']
})
export class VoteFormComponent implements OnInit, OnDestroy {
  voteForm!: FormGroup;
  candidates: Candidate[] = [];
  voters: Voter[] = [];
  isLoading = false;
  private destroy$ = new Subject<void>();

  constructor(
    private formBuilder: FormBuilder,
    private votesService: VotesService,
    private votingRealtimeService: VotingRealtimeService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.voteForm = this.formBuilder.group({
      voterId: ['', Validators.required],
      candidateId: ['', Validators.required]
    });

    this.votingRealtimeService.candidates$
      .pipe(takeUntil(this.destroy$))
      .subscribe(candidates => {
        this.candidates = candidates;
      });

    this.votingRealtimeService.voters$
      .pipe(takeUntil(this.destroy$))
      .subscribe(voters => {
        this.voters = voters;
        this.updateFormState();
      });

    this.votingRealtimeService.error$
      .pipe(takeUntil(this.destroy$))
      .subscribe(error => {
        this.snackBar.open(error, 'Close', { duration: 5000, panelClass: ['error-snackbar'] });
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Updates form state when voters change.
   * Disables submit button if selected voter has already voted.
   */
  private updateFormState(): void {
    // This is handled by isSubmitDisabled()
  }

  /**
   * Submits the vote form.
   */
  submitVote(): void {
    if (!this.voteForm.valid) {
      this.snackBar.open('Please select both a voter and a candidate', 'Close', { duration: 3000 });
      return;
    }

    const selectedVoterId = this.voteForm.get('voterId')?.value;
    const selectedVoter = this.voters.find(v => v.id === selectedVoterId);

    if (selectedVoter?.hasVoted) {
      this.snackBar.open('This voter has already voted', 'Close', { duration: 3000 });
      return;
    }

    this.isLoading = true;
    this.votesService.castVote(
      this.voteForm.get('voterId')!.value,
      this.voteForm.get('candidateId')!.value
    )
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (_response) => {
          this.snackBar.open('Vote cast successfully!', 'Close', { duration: 3000, panelClass: ['success-snackbar'] });
          this.voteForm.reset();
          this.isLoading = false;
        },
        error: (error) => {
          this.isLoading = false;
          if (error.status === 409) {
            this.snackBar.open('This voter has already voted', 'Close', { duration: 5000, panelClass: ['error-snackbar'] });
          } else if (error.status === 404) {
            this.snackBar.open('Voter or candidate not found', 'Close', { duration: 5000, panelClass: ['error-snackbar'] });
          } else {
            this.snackBar.open('Failed to cast vote', 'Close', { duration: 5000, panelClass: ['error-snackbar'] });
          }
        }
      });
  }

  /**
   * Returns whether the submit button should be disabled.
   */
  isSubmitDisabled(): boolean {
    if (!this.voteForm.valid || this.isLoading) {
      return true;
    }
    const selectedVoterId = this.voteForm.get('voterId')?.value;
    const selectedVoter = this.voters.find(v => v.id === selectedVoterId);
    return selectedVoter?.hasVoted ?? false;
  }
}
