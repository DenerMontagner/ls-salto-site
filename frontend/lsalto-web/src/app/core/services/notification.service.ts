import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private snackBar = inject(MatSnackBar);

  sucesso(msg: string): void {
    this.snackBar.open(msg, 'OK', { duration: 3000, panelClass: ['snack-sucesso'] });
  }

  erro(msg: string): void {
    this.snackBar.open(msg, 'Fechar', { duration: 6000, panelClass: ['snack-erro'] });
  }
}
