import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Candidate } from '../../models/candidate.model';
import { VotingRealtimeService } from '../../services/voting-realtime.service';
import { AddCandidateDialogComponent } from '../add-candidate-dialog/add-candidate-dialog.component';

@Component({
    selector: 'app-candidates-table',
    imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatDialogModule],
    templateUrl: './candidates-table.component.html',
    styleUrls: ['./candidates-table.component.css']
})
export class CandidatesTableComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['name', 'voteCount', 'actions'];
  candidates: Candidate[] = [];
  private destroy$ = new Subject<void>();

  constructor(
    private votingRealtimeService: VotingRealtimeService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.votingRealtimeService.candidates$
      .pipe(takeUntil(this.destroy$))
      .subscribe(candidates => {
        this.candidates = candidates;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddCandidateDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.votingRealtimeService.addCandidate(result);
      }
    });
  }
}
