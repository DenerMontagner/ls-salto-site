import { Component, inject, signal, computed } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { PublicadoresService, Publicador } from '../../core/services/publicadores.service';

@Component({
  selector: 'app-grupos',
  imports: [MatProgressSpinnerModule, MatIconModule, MatButtonToggleModule, MatChipsModule],
  templateUrl: './grupos.component.html',
  styleUrl: './grupos.component.scss',
})
export class GruposComponent {
  private service = inject(PublicadoresService);

  loading = signal(true);
  publicadores = signal<Publicador[]>([]);
  grupoFiltro = signal<string>('todos');
  atributos = signal<string[]>([]);

  readonly filtrosAtributos = [
    { valor: 'anciao',            label: 'Ancião' },
    { valor: 'servo',             label: 'Servo Ministerial' },
    { valor: 'pioneiro-regular',  label: 'Pioneiro Regular' },
    { valor: 'pioneiro-auxiliar', label: 'Pioneiro Auxiliar' },
    { valor: 'batizado',          label: 'Batizado' },
    { valor: 'homem',             label: 'Homem' },
    { valor: 'mulher',            label: 'Mulher' },
  ];

  mostrarSalto = computed(() => this.grupoFiltro() !== 'itu');
  mostrarItu   = computed(() => this.grupoFiltro() !== 'salto');

  private salto = computed(() =>
    this.publicadores().filter(p => p.grupoNome === 'Salto').sort((a, b) => a.nome.localeCompare(b.nome))
  );
  private itu = computed(() =>
    this.publicadores().filter(p => p.grupoNome === 'Itu').sort((a, b) => a.nome.localeCompare(b.nome))
  );

  saltoFiltrado = computed(() => this.aplicarFiltros(this.salto()));
  ituFiltrado   = computed(() => this.aplicarFiltros(this.itu()));

  ngOnInit(): void {
    this.service.listar().subscribe({
      next: lista => { this.publicadores.set(lista); this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }

  toggleAtributo(valor: string): void {
    const atual = this.atributos();
    this.atributos.set(
      atual.includes(valor) ? atual.filter(a => a !== valor) : [...atual, valor]
    );
  }

  private aplicarFiltros(lista: Publicador[]): Publicador[] {
    const attrs = this.atributos();
    if (!attrs.length) return lista;
    return lista.filter(p => attrs.every(a => this.corresponde(p, a)));
  }

  private corresponde(p: Publicador, attr: string): boolean {
    switch (attr) {
      case 'anciao':            return p.cargoAtualNome === 'Ancião';
      case 'servo':             return p.cargoAtualNome === 'Servo Ministerial';
      case 'pioneiro-regular':  return p.privilegioAtualNome === 'Pioneiro Regular';
      case 'pioneiro-auxiliar': return p.privilegioAtualNome === 'Pioneiro Auxiliar';
      case 'batizado':          return p.isBatizado;
      case 'homem':             return p.sexo === 'Masculino';
      case 'mulher':            return p.sexo === 'Feminino';
      default:                  return true;
    }
  }

  iniciais(nome: string): string {
    return nome.split(' ').filter(w => w.length > 0).slice(0, 2).map(w => w[0].toUpperCase()).join('');
  }
}
