import { Component, inject, signal, computed } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { MatChipsModule } from '@angular/material/chips';
import { FormsModule } from '@angular/forms';
import { PublicadoresService, Publicador } from '../../core/services/publicadores.service';
import { PublicadorFormDialogComponent } from './publicador-form-dialog/publicador-form-dialog.component';
import { PublicadorDetalheDialogComponent } from './publicador-detalhe-dialog/publicador-detalhe-dialog.component';
import { ConfirmDialogComponent } from '../../shared/confirm-dialog/confirm-dialog.component';
import { NotificationService } from '../../core/services/notification.service';

@Component({
  selector: 'app-publicadores',
  imports: [
    FormsModule,
    MatTableModule, MatButtonModule, MatIconModule, MatInputModule,
    MatFormFieldModule, MatTooltipModule, MatProgressSpinnerModule, MatChipsModule,
  ],
  templateUrl: './publicadores.component.html',
  styleUrl: './publicadores.component.scss',
})
export class PublicadoresComponent {
  private service = inject(PublicadoresService);
  private dialog = inject(MatDialog);
  private notify = inject(NotificationService);

  loading = signal(true);
  publicadores = signal<Publicador[]>([]);
  busca = signal('');

  colunas = ['nome', 'email', 'sexo', 'batizado', 'grupo', 'telefone', 'acoes'];

  filtrados = computed(() => {
    const termo = this.busca().toLowerCase().trim();
    if (!termo) return this.publicadores();
    return this.publicadores().filter(p =>
      p.nome.toLowerCase().includes(termo) ||
      p.emailUsername.toLowerCase().includes(termo)
    );
  });

  ngOnInit(): void {
    this.carregar();
  }

  private carregar(): void {
    this.loading.set(true);
    this.service.listar().subscribe({
      next: (lista) => { this.publicadores.set(lista); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  abrirDetalhe(publicador: Publicador): void {
    const ref = this.dialog.open(PublicadorDetalheDialogComponent, {
      data: publicador,
      width: '440px',
      maxWidth: '95vw',
    });
    ref.afterClosed().subscribe(resultado => {
      if (resultado === 'editar') this.abrirFormulario(publicador);
      else if (resultado === true) this.carregar();
    });
  }

  abrirFormulario(publicador: Publicador | null = null): void {
    const ref = this.dialog.open(PublicadorFormDialogComponent, {
      data: { publicador },
      width: '640px',
      maxWidth: '95vw',
    });
    ref.afterClosed().subscribe(salvou => {
      if (!salvou) return;
      this.notify.sucesso(publicador ? 'Publicador atualizado com sucesso.' : 'Publicador cadastrado com sucesso.');
      this.carregar();
    });
  }

  confirmarExclusao(publicador: Publicador): void {
    const ref = this.dialog.open(ConfirmDialogComponent, {
      data: {
        titulo: 'Excluir publicador',
        mensagem: `Deseja excluir <strong>${publicador.nome}</strong>? Esta ação não pode ser desfeita.`,
      },
    });
    ref.afterClosed().subscribe(confirmou => {
      if (!confirmou) return;
      this.service.excluir(publicador.id).subscribe({
        next: () => {
          this.notify.sucesso('Publicador excluído.');
          this.carregar();
        },
        error: (err: any) => {
          const msg = err?.error?.message ?? 'Erro ao excluir publicador.';
          this.notify.erro(msg);
        },
      });
    });
  }
}
