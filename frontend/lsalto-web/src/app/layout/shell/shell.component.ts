import { Component, inject, signal, computed } from '@angular/core';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { toSignal } from '@angular/core/rxjs-interop';
import { map } from 'rxjs';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthService } from '../../core/auth/auth.service';
import { ThemeService } from '../../core/services/theme.service';

interface NavItem {
  label: string;
  icon: string;
  route: string;
  anciaoOnly: boolean;
}

@Component({
  selector: 'app-shell',
  imports: [
    RouterOutlet, RouterLink, RouterLinkActive,
    MatSidenavModule, MatToolbarModule, MatIconModule,
    MatButtonModule, MatListModule, MatMenuModule, MatTooltipModule,
  ],
  templateUrl: './shell.component.html',
  styleUrl: './shell.component.scss'
})
export class ShellComponent {
  auth = inject(AuthService);
  theme = inject(ThemeService);

  private breakpoint = inject(BreakpointObserver);
  isMobile = toSignal(
    this.breakpoint.observe(Breakpoints.Handset).pipe(map(r => r.matches)),
    { initialValue: false }
  );

  isDark = this.theme.isDark;

  nome = computed(() => this.auth.getNome());
  isAnciao = computed(() => this.auth.isAnciao());

  readonly navItems: NavItem[] = [
    { label: 'Home',                  icon: 'home',               route: '/dashboard',    anciaoOnly: false },
    { label: 'Reunião MWB',            icon: 'event_note',         route: '/reuniao-mwb',     anciaoOnly: false },
    { label: 'Reunião Domingo',        icon: 'event_available',    route: '/reuniao-domingo', anciaoOnly: false },
    { label: 'Limpeza',               icon: 'cleaning_services',  route: '/limpeza',      anciaoOnly: false },
    { label: 'Partes Mecânicas',      icon: 'settings',           route: '/mecanicas',    anciaoOnly: false },
    { label: 'Pregação',              icon: 'record_voice_over',  route: '/pregacao',     anciaoOnly: false },
    { label: 'Cadastros',             icon: 'people',             route: '/publicadores', anciaoOnly: true  },
    { label: 'Designações',           icon: 'assignment',         route: '/designacoes',  anciaoOnly: true  },
    { label: 'Grupos',                icon: 'groups',             route: '/grupos',       anciaoOnly: true  },
  ];

  visibleItems = computed(() =>
    this.navItems.filter(i => !i.anciaoOnly || this.isAnciao())
  );

  toggleTheme(): void {
    const dark = !this.isDark();
    this.isDark.set(dark);
    document.body.classList.toggle('dark-mode', dark);
    localStorage.setItem('lsalto_theme', dark ? 'dark' : 'light');
  }

  logout(): void {
    this.auth.logout();
  }

  ngOnInit(): void {
    if (this.isDark()) document.body.classList.add('dark-mode');
  }
}
