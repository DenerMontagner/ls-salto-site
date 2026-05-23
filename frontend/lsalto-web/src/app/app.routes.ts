import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { anciaoGuard } from './core/guards/anciao.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: '',
    loadComponent: () => import('./layout/shell/shell.component').then(m => m.ShellComponent),
    canActivate: [authGuard],
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      {
        path: 'dashboard',
        loadComponent: () => import('./features/dashboard/dashboard.component').then(m => m.DashboardComponent)
      },
      {
        path: 'publicadores',
        canActivate: [anciaoGuard],
        loadComponent: () => import('./features/publicadores/publicadores.component').then(m => m.PublicadoresComponent),
      },
      {
        path: 'designacoes',
        canActivate: [anciaoGuard],
        loadComponent: () => import('./features/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { titulo: 'Gestão de Designações' }
      },
      {
        path: 'grupos',
        canActivate: [anciaoGuard],
        loadComponent: () => import('./features/grupos/grupos.component').then(m => m.GruposComponent),
      },
      {
        path: 'reuniao-mwb',
        loadComponent: () => import('./features/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { titulo: 'Reunião Vida e Ministério' }
      },
      {
        path: 'reuniao-domingo',
        loadComponent: () => import('./features/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { titulo: 'Reunião de Fim de Semana' }
      },
      {
        path: 'limpeza',
        loadComponent: () => import('./features/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { titulo: 'Limpeza' }
      },
      {
        path: 'mecanicas',
        loadComponent: () => import('./features/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { titulo: 'Partes Mecânicas' }
      },
      {
        path: 'pregacao',
        loadComponent: () => import('./features/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { titulo: 'Pregação' }
      },
      {
        path: 'anuncios',
        loadComponent: () => import('./features/placeholder/placeholder.component').then(m => m.PlaceholderComponent),
        data: { titulo: 'Anúncios' }
      },
    ]
  },
  { path: '**', redirectTo: '' }
];
