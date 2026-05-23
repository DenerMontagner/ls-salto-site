import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { environment } from '../../../environments/environment';

interface DesignacaoDto {
  id: number;
  tipoDesignacao: string;
  data: string;
  publicadorTitular: string;
  publicadorAjudante?: string;
  grupo?: string;
}

interface AnuncioDto {
  id: number;
  descricao: string;
  dataCriacao: string;
}

interface DashboardDto {
  proximasDesignacoes: DesignacaoDto[];
  ultimosAnuncios: AnuncioDto[];
}

const FILTROS = [
  { label: 'Demonstração/Discurso', tipos: ['Demonstração (Titular)', 'Demonstração (Ajudante)', 'Discurso Tesouros', 'Discurso Vida Cristã'] },
  { label: 'Mecânicas',             tipos: ['Mecânicas', 'Presidente', 'Estudo Bíblico (EBC)'] },
  { label: 'Limpeza',               tipos: ['Limpeza'] },
  { label: 'Pregação',              tipos: ['Pregação de Campo'] },
];

@Component({
  selector: 'app-dashboard',
  imports: [
    DatePipe,
    MatCardModule, MatButtonToggleModule, MatIconModule,
    MatProgressSpinnerModule, MatChipsModule, MatDividerModule,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  private http = inject(HttpClient);

  loading = signal(true);
  dashboard = signal<DashboardDto | null>(null);
  filtroAtivo = signal(FILTROS[0].label);
  filtros = FILTROS;

  designacoesFiltradas = computed(() => {
    const d = this.dashboard();
    if (!d) return [];
    const filtro = this.filtros.find(f => f.label === this.filtroAtivo());
    if (!filtro) return d.proximasDesignacoes;
    return d.proximasDesignacoes.filter(x => filtro.tipos.includes(x.tipoDesignacao));
  });

  ngOnInit(): void {
    this.http.get<DashboardDto>(`${environment.apiUrl}/api/dashboard/me`).subscribe({
      next: data => { this.dashboard.set(data); this.loading.set(false); },
      error: () => this.loading.set(false)
    });
  }
}
