import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { CandidatesService } from '../../services/candidates.service';

@Component({
    selector: 'app-add-candidate-dialog',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatSnackBarModule
    ],
    templateUrl: './add-candidate-dialog.component.html',
    styleUrls: ['./add-candidate-dialog.component.css']
})
export class AddCandidateDialogComponent {
  form: FormGroup;
  isLoading = false;

  constructor(
    private formBuilder: FormBuilder,
    private candidatesService: CandidatesService,
    private snackBar: MatSnackBar,
    public dialogRef: MatDialogRef<AddCandidateDialogComponent>
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
      this.snackBar.open('Please enter a candidate name', 'Close', { duration: 3000 });
      return;
    }

    this.isLoading = true;
    this.candidatesService.createCandidate(this.form.get('name')!.value)
      .subscribe({
        next: (candidate) => {
          this.snackBar.open('Candidate added successfully!', 'Close', { duration: 3000 });
          this.dialogRef.close(candidate);
        },
        error: (_error) => {
          this.isLoading = false;
          this.snackBar.open('Failed to add candidate', 'Close', { duration: 5000 });
        }
      });
  }
}
