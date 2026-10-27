import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Novedad, NovedadRequest } from '../models/novedad.model';
import { NovedadService } from '../services/novedad.service';

@Component({
  imports: [FormsModule],
  selector: 'app-admin-novedades',
  styleUrl: './admin-novedades.css',
  templateUrl: './admin-novedades.html',
})
export class AdminNovedades {
  private readonly novedadService = inject(NovedadService);

  novedades = signal<Novedad[]>([]);
  novedadNueva: NovedadRequest = this.crearNovedadVacia();
  editandoId = signal<number | null>(null);
  guardando = signal(false);
  aviso = signal('');
  error = signal('');

  constructor() {
    this.cargarNovedades();
  }

  cargarNovedades(): void {
    this.novedadService.obtenerNovedades().subscribe({
      next: (novedades) => this.novedades.set(novedades),
      error: () => this.error.set('No se pudieron cargar las novedades.'),
    });
  }

  seleccionarParaEditar(novedad: Novedad): void {
    this.editandoId.set(novedad.id);
    this.novedadNueva = {
      titulo: novedad.titulo,
      resumen: novedad.resumen,
      contenido: novedad.contenido,
      imagenUrl: novedad.imagenUrl,
      estaPublicada: novedad.estaPublicada,
    };
    this.aviso.set('');
    this.error.set('');
  }

  cancelarEdicion(): void {
    this.editandoId.set(null);
    this.novedadNueva = this.crearNovedadVacia();
  }

  eliminarNovedad(id: number): void {
    this.aviso.set('');
    this.error.set('');

    this.novedadService.eliminarNovedad(id).subscribe({
      next: () => {
        if (this.editandoId() === id) {
          this.cancelarEdicion();
        }
        this.aviso.set('Novedad eliminada correctamente.');
        this.cargarNovedades();
      },
      error: () => this.error.set('No se pudo eliminar la novedad.'),
    });
  }

  guardarNovedad(): void {
    this.guardando.set(true);
    this.aviso.set('');
    this.error.set('');

    const id = this.editandoId();
    const peticion = id === null
      ? this.novedadService.crearNovedad(this.novedadNueva)
      : this.novedadService.actualizarNovedad(id, this.novedadNueva);

    peticion.subscribe({
      next: () => {
        this.novedadNueva = this.crearNovedadVacia();
        this.editandoId.set(null);
        this.aviso.set(id === null ? 'Novedad agregada correctamente.' : 'Novedad actualizada correctamente.');
        this.guardando.set(false);
        this.cargarNovedades();
      },
      error: () => {
        this.error.set(id === null ? 'No se pudo agregar la novedad.' : 'No se pudo actualizar la novedad.');
        this.guardando.set(false);
      },
    });
  }

  private crearNovedadVacia(): NovedadRequest {
    return {
      titulo: '',
      resumen: '',
      contenido: '',
      imagenUrl: null,
      estaPublicada: false,
    };
  }
}
