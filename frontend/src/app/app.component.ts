import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { ReactiveFormsModule } from '@angular/forms';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { CandidatesTableComponent } from './components/candidates-table/candidates-table.component';
import { VotersTableComponent } from './components/voters-table/voters-table.component';
import { VoteFormComponent } from './components/vote-form/vote-form.component';
import { VotingRealtimeService } from './services/voting-realtime.service';

@Component({
    selector: 'app-root',
    imports: [
        CommonModule,
        MatTabsModule,
        MatTableModule,
        MatButtonModule,
        MatIconModule,
        MatSelectModule,
        MatFormFieldModule,
        MatSnackBarModule,
        ReactiveFormsModule,
        CandidatesTableComponent,
        VotersTableComponent,
        VoteFormComponent
    ],
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();

  constructor(private votingRealtimeService: VotingRealtimeService) {}

  ngOnInit(): void {
    // Initialize real-time connection
    this.votingRealtimeService.connect()
      .pipe(takeUntil(this.destroy$))
      .subscribe();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
