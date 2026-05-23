import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-placeholder',
  imports: [MatIconModule],
  template: `
    <div class="placeholder">
      <mat-icon>construction</mat-icon>
      <h2>{{ titulo }}</h2>
      <p>Esta tela está em desenvolvimento.</p>
    </div>
  `,
  styles: [`
    .placeholder {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      min-height: 300px;
      gap: 12px;
      color: var(--mat-sys-on-surface-variant);

      mat-icon { font-size: 64px; width: 64px; height: 64px; }
      h2 { margin: 0; font-size: 1.5rem; }
      p  { margin: 0; }
    }
  `]
})
export class PlaceholderComponent {
  titulo = inject(ActivatedRoute).snapshot.data['titulo'] ?? 'Em construção';
}
