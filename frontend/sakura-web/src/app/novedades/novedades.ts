import { Component, inject, signal } from '@angular/core';
import { Novedad } from '../models/novedad.model';
import { NovedadService } from '../services/novedad.service';

@Component({
  imports: [],
  selector: 'app-novedades',
  styleUrl: './novedades.css',
  templateUrl: './novedades.html',
})
export class Novedades {
  private readonly novedadService = inject(NovedadService);

  novedades = signal<Novedad[]>([]);
  cargando = signal(true);
  error = signal('');

  constructor() {
    this.novedadService.obtenerNovedades().subscribe({
      next: (novedades) => {
        this.novedades.set(novedades.filter((novedad) => novedad.estaPublicada));
        this.cargando.set(false);
      },
      error: () => {
        this.error.set('No se pudieron cargar las novedades.');
        this.cargando.set(false);
      },
    });
  }
}
