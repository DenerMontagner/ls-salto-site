import { Injectable, signal } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ThemeService {
  isDark = signal(localStorage.getItem('lsalto_theme') === 'dark');

  constructor() {
    this.apply(this.isDark());
  }

  toggle(): void {
    this.apply(!this.isDark());
  }

  private apply(dark: boolean): void {
    this.isDark.set(dark);
    document.body.classList.toggle('dark-mode', dark);
    localStorage.setItem('lsalto_theme', dark ? 'dark' : 'light');
  }
}
