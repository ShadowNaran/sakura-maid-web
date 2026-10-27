import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Evento, EventoRequest } from '../models/evento.model';
import { EventoService } from '../services/evento.service';

@Component({
  imports: [FormsModule],
  selector: 'app-admin-eventos',
  styleUrl: './admin-eventos.css',
  templateUrl: './admin-eventos.html',
})
export class AdminEventos {
  private readonly eventoService = inject(EventoService);

  eventos = signal<Evento[]>([]);
  eventoNuevo: EventoRequest = this.crearEventoVacio();
  editandoId = signal<number | null>(null);
  guardando = signal(false);
  aviso = signal('');
  error = signal('');

  constructor() {
    this.cargarEventos();
  }

  cargarEventos(): void {
    this.eventoService.obtenerEventos().subscribe({
      next: (eventos) => this.eventos.set(eventos),
      error: () => this.error.set('No se pudieron cargar los eventos.'),
    });
  }

  seleccionarParaEditar(evento: Evento): void {
    this.editandoId.set(evento.id);
    this.eventoNuevo = {
      titulo: evento.titulo,
      descripcion: evento.descripcion,
      iniciaEn: this.formatearFechaLocal(evento.iniciaEn),
      ubicacion: evento.ubicacion,
      aficheUrl: evento.aficheUrl,
      estado: evento.estado,
    };
    this.aviso.set('');
    this.error.set('');
  }

  cancelarEdicion(): void {
    this.editandoId.set(null);
    this.eventoNuevo = this.crearEventoVacio();
  }

  eliminarEvento(id: number): void {
    this.aviso.set('');
    this.error.set('');

    this.eventoService.eliminarEvento(id).subscribe({
      next: () => {
        if (this.editandoId() === id) {
          this.cancelarEdicion();
        }
        this.aviso.set('Evento eliminado correctamente.');
        this.cargarEventos();
      },
      error: () => this.error.set('No se pudo eliminar el evento.'),
    });
  }

  guardarEvento(): void {
    this.guardando.set(true);
    this.aviso.set('');
    this.error.set('');

    const evento = {
      ...this.eventoNuevo,
      iniciaEn: new Date(this.eventoNuevo.iniciaEn).toISOString(),
    };

    const id = this.editandoId();
    const peticion = id === null
      ? this.eventoService.crearEvento(evento)
      : this.eventoService.actualizarEvento(id, evento);

    peticion.subscribe({
      next: () => {
        this.eventoNuevo = this.crearEventoVacio();
        this.editandoId.set(null);
        this.aviso.set(id === null ? 'Evento agregado correctamente.' : 'Evento actualizado correctamente.');
        this.guardando.set(false);
        this.cargarEventos();
      },
      error: () => {
        this.error.set(id === null ? 'No se pudo agregar el evento.' : 'No se pudo actualizar el evento.');
        this.guardando.set(false);
      },
    });
  }

  private formatearFechaLocal(fechaIso: string): string {
    const fecha = new Date(fechaIso);
    if (Number.isNaN(fecha.getTime())) {
      return '';
    }
    const ajuste = fecha.getTimezoneOffset() * 60000;
    return new Date(fecha.getTime() - ajuste).toISOString().slice(0, 16);
  }

  private crearEventoVacio(): EventoRequest {
    return {
      titulo: '',
      descripcion: '',
      iniciaEn: '',
      ubicacion: null,
      aficheUrl: null,
      estado: 'Published',
    };
  }
}
