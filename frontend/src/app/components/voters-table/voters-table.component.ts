import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Voter } from '../../models/voter.model';
import { VotingRealtimeService } from '../../services/voting-realtime.service';
import { AddVoterDialogComponent } from '../add-voter-dialog/add-voter-dialog.component';

@Component({
    selector: 'app-voters-table',
    imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatDialogModule],
    templateUrl: './voters-table.component.html',
    styleUrls: ['./voters-table.component.css']
})
export class VotersTableComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = ['name', 'hasVoted', 'actions'];
  voters: Voter[] = [];
  private destroy$ = new Subject<void>();

  constructor(
    private votingRealtimeService: VotingRealtimeService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.votingRealtimeService.voters$
      .pipe(takeUntil(this.destroy$))
      .subscribe(voters => {
        this.voters = voters;
      });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddVoterDialogComponent);
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.votingRealtimeService.addVoter(result);
      }
    });
  }

  getVotedIcon(hasVoted: boolean): string {
    return hasVoted ? 'check_circle' : 'cancel';
  }

  getVotedColor(hasVoted: boolean): string {
    return hasVoted ? 'accent' : '';
  }
}
