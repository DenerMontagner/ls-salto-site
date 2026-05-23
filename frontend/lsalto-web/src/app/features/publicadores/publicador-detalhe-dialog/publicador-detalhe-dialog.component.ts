import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { Publicador } from '../../../core/services/publicadores.service';

@Component({
  selector: 'app-publicador-detalhe-dialog',
  imports: [MatDialogModule, MatButtonModule, MatIconModule, MatDividerModule],
  templateUrl: './publicador-detalhe-dialog.component.html',
  styleUrl: './publicador-detalhe-dialog.component.scss',
})
export class PublicadorDetalheDialogComponent {
  private dialogRef = inject(MatDialogRef<PublicadorDetalheDialogComponent>);
  pub: Publicador = inject(MAT_DIALOG_DATA);

  iniciais = this.pub.nome
    .split(' ')
    .filter(w => w.length > 0)
    .slice(0, 2)
    .map(w => w[0].toUpperCase())
    .join('');

  formatDate(iso: string | null | undefined): string {
    if (!iso) return '—';
    const [y, m, d] = iso.split('-');
    return `${d}/${m}/${y}`;
  }

  editar(): void {
    this.dialogRef.close('editar');
  }
}
