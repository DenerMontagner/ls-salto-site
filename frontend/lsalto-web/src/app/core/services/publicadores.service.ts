import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

export interface Grupo {
  id: number;
  nome: string;
}

export interface Publicador {
  id: number;
  nome: string;
  emailUsername: string;
  sexo: string;
  isBatizado: boolean;
  dataNascimento: string;
  dataBatismo: string | null;
  telefone: string | null;
  endereco: string | null;
  cargoAtualId: number | null;
  cargoAtualNome: string | null;
  cargoDataInicio: string | null;
  privilegioAtualId: number | null;
  privilegioAtualNome: string | null;
  privilegioDataInicio: string | null;
  grupoId: number | null;
  grupoNome: string | null;
}

export interface CriarPublicadorDto {
  nome: string; emailUsername: string; senha: string; sexo: string;
  dataNascimento: string; dataBatismo: string | null;
  telefone: string | null; endereco: string | null;
  idGrupo: number | null;
}

export interface AtualizarPublicadorDto {
  id: number; nome: string; emailUsername: string; sexo: string;
  dataNascimento: string; dataBatismo: string | null;
  telefone: string | null; endereco: string | null;
  idGrupo: number | null;
}

export interface AtribuirCargoDto   { idCargo: number;     dataInicio: string; }
export interface AtribuirPrivilegioDto { idPrivilegio: number; dataInicio: string; }

@Injectable({ providedIn: 'root' })
export class PublicadoresService {
  private http = inject(HttpClient);
  private url = `${environment.apiUrl}/api/publicadores`;
  private gruposUrl = `${environment.apiUrl}/api/grupos`;

  listarGrupos(): Observable<Grupo[]> {
    return this.http.get<Grupo[]>(this.gruposUrl);
  }

  listar(): Observable<Publicador[]> {
    return this.http.get<Publicador[]>(this.url);
  }

  criar(dto: CriarPublicadorDto): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.url, dto);
  }

  atualizar(id: number, dto: AtualizarPublicadorDto): Observable<void> {
    return this.http.put<void>(`${this.url}/${id}`, dto);
  }

  excluir(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }

  atribuirCargo(id: number, dto: AtribuirCargoDto): Observable<void> {
    return this.http.post<void>(`${this.url}/${id}/cargo`, dto);
  }

  removerCargo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}/cargo`);
  }

  atribuirPrivilegio(id: number, dto: AtribuirPrivilegioDto): Observable<void> {
    return this.http.post<void>(`${this.url}/${id}/privilegio`, dto);
  }

  removerPrivilegio(id: number): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}/privilegio`);
  }
}
