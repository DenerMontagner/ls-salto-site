import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap, map } from 'rxjs';
import { environment } from '../../../environments/environment';

interface LoginResponse {
  token: string;
  nome: string;
  role: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private http = inject(HttpClient);
  private router = inject(Router);
  private tokenKey = 'lsalto_token';

  login(email: string, senha: string): Observable<void> {
    return this.http
      .post<LoginResponse>(`${environment.apiUrl}/api/auth/login`, { email, senha })
      .pipe(
        tap(r => localStorage.setItem(this.tokenKey, r.token)),
        map(() => void 0)
      );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (!token) return false;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.exp * 1000 > Date.now();
    } catch {
      return false;
    }
  }

  private getPayload(): Record<string, string> | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch {
      return null;
    }
  }

  getRole(): string { return this.getPayload()?.['role'] ?? ''; }
  getNome(): string { return this.getPayload()?.['name'] ?? ''; }
  getId(): number   { return Number(this.getPayload()?.['sub'] ?? 0); }
  isAnciao(): boolean { return this.getRole() === 'Anciao'; }
}
