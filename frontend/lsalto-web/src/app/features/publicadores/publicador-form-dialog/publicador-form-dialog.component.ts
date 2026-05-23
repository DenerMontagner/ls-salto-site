import { Component, inject, signal } from '@angular/core';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { forkJoin, Observable, of } from 'rxjs';
import { PublicadoresService, Publicador, Grupo } from '../../../core/services/publicadores.service';

export interface PublicadorDialogData { publicador: Publicador | null; }

@Component({
  selector: 'app-publicador-form-dialog',
  imports: [
    ReactiveFormsModule,
    MatDialogModule, MatFormFieldModule, MatInputModule, MatSelectModule,
    MatButtonModule, MatIconModule, MatProgressSpinnerModule,
    MatDatepickerModule, MatDividerModule, MatCheckboxModule,
  ],
  templateUrl: './publicador-form-dialog.component.html',
  styleUrl: './publicador-form-dialog.component.scss',
})
export class PublicadorFormDialogComponent {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<PublicadorFormDialogComponent>);
  private service = inject(PublicadoresService);
  data: PublicadorDialogData = inject(MAT_DIALOG_DATA);

  isEdit = !!this.data.publicador;
  loading = signal(false);
  erro = signal('');
  senhaVisivel = signal(false);
  temCargo = signal(!!this.data.publicador?.cargoAtualId);
  temPrivilegio = signal(!!this.data.publicador?.privilegioAtualId);
  grupos = signal<Grupo[]>([]);

  constructor() {
    this.service.listarGrupos().subscribe(lista => this.grupos.set(lista));
  }

  private parseDate(iso: string | null | undefined): Date | null {
    if (!iso) return null;
    const [y, m, d] = iso.split('-').map(Number);
    return new Date(y, m - 1, d);
  }

  form = this.fb.group({
    nome:           [this.data.publicador?.nome ?? '', Validators.required],
    emailUsername:  [this.data.publicador?.emailUsername ?? '', [Validators.required, Validators.email]],
    senha:          ['', this.isEdit ? [] : [Validators.required, Validators.minLength(6)]],
    sexo:           [this.data.publicador?.sexo ?? '', Validators.required],
    dataNascimento: [this.parseDate(this.data.publicador?.dataNascimento) as Date | null, Validators.required],
    dataBatismo:    [this.parseDate(this.data.publicador?.dataBatismo) as Date | null],
    telefone:       [this.data.publicador?.telefone ?? ''],
    endereco:       [this.data.publicador?.endereco ?? ''],
    cargo:          [this.data.publicador?.cargoAtualId?.toString() ?? ''],
    cargoDataInicio:[this.parseDate(this.data.publicador?.cargoDataInicio) as Date | null],
    privilegio:     [this.data.publicador?.privilegioAtualId?.toString() ?? ''],
    privDataInicio: [this.parseDate(this.data.publicador?.privilegioDataInicio) as Date | null],
    grupo:          [this.data.publicador?.grupoId?.toString() ?? '', Validators.required],
  });

  mascaraTelefone(event: Event): void {
    const input = event.target as HTMLInputElement;
    let digits = input.value.replace(/\D/g, '').slice(0, 11);
    let f = digits;
    if (digits.length > 2) f = `(${digits.slice(0, 2)}) ${digits.slice(2)}`;
    if (digits.length > 7) f = `(${digits.slice(0, 2)}) ${digits.slice(2, 7)}-${digits.slice(7)}`;
    input.value = f;
    this.form.get('telefone')!.setValue(f, { emitEvent: false });
  }

  mascaraData(event: Event): void {
    const input = event.target as HTMLInputElement;
    const digits = input.value.replace(/\D/g, '').slice(0, 8);
    let f = digits;
    if (digits.length > 2) f = `${digits.slice(0, 2)}/${digits.slice(2)}`;
    if (digits.length > 4) f = `${digits.slice(0, 2)}/${digits.slice(2, 4)}/${digits.slice(4)}`;
    input.value = f;
  }

  salvar(): void {
    if (this.form.invalid || this.loading()) return;
    this.loading.set(true);
    this.erro.set('');

    const v = this.form.value;
    const toDateStr = (d: Date | null | undefined): string | null =>
      d ? `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}-${String(d.getDate()).padStart(2, '0')}` : null;

    const idGrupo = v.grupo ? parseInt(v.grupo) : null;

    const dadosOp$: Observable<unknown> = this.isEdit
      ? this.service.atualizar(this.data.publicador!.id, {
          id: this.data.publicador!.id,
          nome: v.nome!, emailUsername: v.emailUsername!, sexo: v.sexo!,
          dataNascimento: toDateStr(v.dataNascimento)!,
          dataBatismo: toDateStr(v.dataBatismo),
          telefone: v.telefone || null, endereco: v.endereco || null,
          idGrupo,
        })
      : this.service.criar({
          nome: v.nome!, emailUsername: v.emailUsername!, senha: v.senha!, sexo: v.sexo!,
          dataNascimento: toDateStr(v.dataNascimento)!,
          dataBatismo: toDateStr(v.dataBatismo),
          telefone: v.telefone || null, endereco: v.endereco || null,
          idGrupo,
        });

    dadosOp$.subscribe({
      next: (res: any) => {
        const id = this.isEdit ? this.data.publicador!.id : res?.id;
        this.salvarCargoPrivilegio(id, v, toDateStr);
      },
      error: (err: any) => {
        const msg = err?.error?.message ?? err?.message ?? `Erro ${err?.status ?? ''}: verifique se a API está rodando.`;
        this.erro.set(msg);
        this.loading.set(false);
      },
    });
  }

  private salvarCargoPrivilegio(id: number, v: any, toDateStr: (d: Date | null | undefined) => string | null): void {
    const pub = this.data.publicador;
    const ops: Observable<void>[] = [];

    const novoCargoId    = this.temCargo() && v.cargo ? parseInt(v.cargo) : null;
    const cargoAnterior  = pub?.cargoAtualId ?? null;
    const novaCargoData  = toDateStr(v.cargoDataInicio);
    const cargoMudou     = novoCargoId !== cargoAnterior || (novoCargoId && novaCargoData !== (pub?.cargoDataInicio ?? null));
    if (cargoMudou) {
      if (novoCargoId && v.cargoDataInicio) {
        ops.push(this.service.atribuirCargo(id, { idCargo: novoCargoId, dataInicio: novaCargoData! }));
      } else if (!novoCargoId && cargoAnterior) {
        ops.push(this.service.removerCargo(id));
      }
    }

    const novoPrivId   = this.temPrivilegio() && v.privilegio ? parseInt(v.privilegio) : null;
    const privAnterior = pub?.privilegioAtualId ?? null;
    const novaPrivData = toDateStr(v.privDataInicio);
    const privMudou    = novoPrivId !== privAnterior || (novoPrivId && novaPrivData !== (pub?.privilegioDataInicio ?? null));
    if (privMudou) {
      if (novoPrivId && v.privDataInicio) {
        ops.push(this.service.atribuirPrivilegio(id, { idPrivilegio: novoPrivId, dataInicio: novaPrivData! }));
      } else if (!novoPrivId && privAnterior) {
        ops.push(this.service.removerPrivilegio(id));
      }
    }

    (ops.length ? forkJoin(ops) as Observable<unknown> : of(null)).subscribe({
      next: () => this.dialogRef.close(true),
      error: (err: any) => {
        const msg = err?.error?.message ?? err?.message ?? 'Erro ao salvar cargo/privilégio.';
        this.erro.set(msg);
        this.loading.set(false);
      },
    });
  }
}
