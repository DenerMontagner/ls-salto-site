import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { DateAdapter, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import { BrDateAdapter } from './core/services/br-date-adapter';
import { routes } from './app.routes';
import { jwtInterceptor } from './core/interceptors/jwt.interceptor';

export const BR_DATE_FORMATS = {
  parse:   { dateInput: 'dd/MM/yyyy' },
  display: {
    dateInput:          'dd/MM/yyyy',
    monthYearLabel:     'MMM yyyy',
    dateA11yLabel:      'dd/MM/yyyy',
    monthYearA11yLabel: 'MMMM yyyy',
  },
};

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    provideHttpClient(withInterceptors([jwtInterceptor])),
    provideAnimationsAsync(),
    { provide: MAT_DATE_LOCALE,   useValue: 'pt-BR' },
    { provide: MAT_DATE_FORMATS,  useValue: BR_DATE_FORMATS },
    { provide: DateAdapter, useClass: BrDateAdapter, deps: [MAT_DATE_LOCALE] },
  ]
};
