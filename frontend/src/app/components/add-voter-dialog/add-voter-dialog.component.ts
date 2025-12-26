import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { VotersService } from '../../services/voters.service';

@Component({
    selector: 'app-add-voter-dialog',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSnackBarModule
    ],
    templateUrl: './add-voter-dialog.component.html',
    styleUrls: ['./add-voter-dialog.component.css']
})
export class AddVoterDialogComponent {
  form: FormGroup;
  isLoading = false;

  constructor(
    private formBuilder: FormBuilder,
    private votersService: VotersService,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<AddVoterDialogComponent>
  ) {
    this.form = this.formBuilder.group({
      name: ['', [Validators.required, Validators.maxLength(255)]]
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onAdd(): void {
    if (!this.form.valid) {
      this.snackBar.open('Please enter a voter name', 'Close', { duration: 3000 });
      return;
    }

    this.isLoading = true;
    this.votersService.createVoter(this.form.get('name')!.value)
      .subscribe({
        next: (voter) => {
          this.snackBar.open('Voter added successfully!', 'Close', { duration: 3000 });
          this.dialogRef.close(voter);
        },
        error: (_error) => {
          this.isLoading = false;
          this.snackBar.open('Failed to add voter', 'Close', { duration: 5000 });
        }
      });
  }
}
