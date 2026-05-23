import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AuthService } from '../../../core/auth/auth.service';
import { ThemeService } from '../../../core/services/theme.service';

@Component({
  selector: 'app-login',
  imports: [
    ReactiveFormsModule,
    MatCardModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatProgressBarModule,
    MatTooltipModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private auth = inject(AuthService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  theme = inject(ThemeService);
  isDark = this.theme.isDark;

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    senha: ['', Validators.required],
  });

  loading = signal(false);
  erro = signal('');
  senhaVisivel = signal(false);

  login(): void {
    if (this.form.invalid || this.loading()) return;
    this.loading.set(true);
    this.erro.set('');

    const { email, senha } = this.form.value;
    this.auth.login(email!, senha!).subscribe({
      next: () => this.router.navigate(['/']),
      error: () => {
        this.erro.set('Email ou senha inválidos.');
        this.loading.set(false);
      }
    });
  }
}
