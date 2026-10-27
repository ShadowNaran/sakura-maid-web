import { DatePipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { Evento } from '../models/evento.model';
import { EventoService } from '../services/evento.service';

@Component({
  imports: [DatePipe],
  selector: 'app-eventos',
  styleUrl: './eventos.css',
  templateUrl: './eventos.html',
})
export class Eventos {
  private readonly eventoService = inject(EventoService);

  eventos = signal<Evento[]>([]);
  cargando = signal(true);
  error = signal('');

  constructor() {
    this.eventoService.obtenerEventos().subscribe({
      next: (eventos) => {
        this.eventos.set(eventos);
        this.cargando.set(false);
      },
      error: () => {
        this.error.set('No se pudieron cargar los eventos.');
        this.cargando.set(false);
      },
    });
  }
}
