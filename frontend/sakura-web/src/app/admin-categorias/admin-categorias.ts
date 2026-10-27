import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Categoria, CategoriaRequest } from '../models/categoria.model';
import { CategoriaService } from '../services/categoria.service';

@Component({
  imports: [FormsModule],
  selector: 'app-admin-categorias',
  styleUrl: './admin-categorias.css',
  templateUrl: './admin-categorias.html',
})
export class AdminCategorias {
  private readonly categoriaService = inject(CategoriaService);

  categorias = signal<Categoria[]>([]);
  categoriaNueva: CategoriaRequest = this.crearCategoriaVacia();
  editandoId = signal<number | null>(null);
  guardando = signal(false);
  aviso = signal('');
  error = signal('');

  constructor() {
    this.cargarCategorias();
  }

  cargarCategorias(): void {
    this.categoriaService.obtenerCategorias().subscribe({
      next: (categorias) => this.categorias.set(categorias),
      error: () => this.error.set('No se pudieron cargar las categorías.'),
    });
  }

  seleccionarParaEditar(categoria: Categoria): void {
    this.editandoId.set(categoria.id);
    this.categoriaNueva = {
      nombre: categoria.nombre,
      descripcion: categoria.descripcion,
    };
    this.aviso.set('');
    this.error.set('');
  }

  cancelarEdicion(): void {
    this.editandoId.set(null);
    this.categoriaNueva = this.crearCategoriaVacia();
  }

  eliminarCategoria(id: number): void {
    this.aviso.set('');
    this.error.set('');

    this.categoriaService.eliminarCategoria(id).subscribe({
      next: () => {
        if (this.editandoId() === id) {
          this.cancelarEdicion();
        }
        this.aviso.set('Categoría eliminada correctamente.');
        this.cargarCategorias();
      },
      error: () => this.error.set('No se pudo eliminar la categoría.'),
    });
  }

  guardarCategoria(): void {
    this.guardando.set(true);
    this.aviso.set('');
    this.error.set('');

    const id = this.editandoId();
    const peticion = id === null
      ? this.categoriaService.crearCategoria(this.categoriaNueva)
      : this.categoriaService.actualizarCategoria(id, this.categoriaNueva);

    peticion.subscribe({
      next: () => {
        this.categoriaNueva = this.crearCategoriaVacia();
        this.editandoId.set(null);
        this.aviso.set(id === null ? 'Categoría agregada correctamente.' : 'Categoría actualizada correctamente.');
        this.guardando.set(false);
        this.cargarCategorias();
      },
      error: () => {
        this.error.set(id === null ? 'No se pudo agregar la categoría.' : 'No se pudo actualizar la categoría.');
        this.guardando.set(false);
      },
    });
  }

  private crearCategoriaVacia(): CategoriaRequest {
    return { nombre: '', descripcion: null };
  }
}
