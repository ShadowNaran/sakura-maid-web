import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Categoria } from '../models/categoria.model';
import { Producto, ProductoRequest } from '../models/producto.model';
import { CategoriaService } from '../services/categoria.service';
import { ProductoService } from '../services/producto.service';

@Component({
  imports: [FormsModule],
  selector: 'app-admin-productos',
  styleUrl: './admin-productos.css',
  templateUrl: './admin-productos.html',
})
export class AdminProductos {
  private readonly productoService = inject(ProductoService);
  private readonly categoriaService = inject(CategoriaService);

  productos = signal<Producto[]>([]);
  categorias = signal<Categoria[]>([]);
  productoNuevo: ProductoRequest = this.crearProductoVacio();
  editandoId = signal<number | null>(null);
  guardando = signal(false);
  aviso = signal('');
  error = signal('');

  constructor() {
    this.cargarProductos();
    this.categoriaService.obtenerCategorias().subscribe({
      next: (categorias) => this.categorias.set(categorias),
      error: () => this.error.set('No se pudieron cargar las categorías.'),
    });
  }

  cargarProductos(): void {
    this.productoService.obtenerProductos().subscribe({
      next: (productos) => this.productos.set(productos),
      error: () => this.error.set('No se pudieron cargar los productos.'),
    });
  }

  seleccionarParaEditar(producto: Producto): void {
    this.editandoId.set(producto.id);
    this.productoNuevo = {
      nombre: producto.nombre,
      descripcion: producto.descripcion,
      precio: producto.precio,
      categoriaId: producto.categoriaId,
      imagenUrl: producto.imagenUrl,
      esDestacado: producto.esDestacado,
      estaDisponible: producto.estaDisponible,
    };
    this.aviso.set('');
    this.error.set('');
  }

  cancelarEdicion(): void {
    this.editandoId.set(null);
    this.productoNuevo = this.crearProductoVacio();
  }

  eliminarProducto(id: number): void {
    this.aviso.set('');
    this.error.set('');

    this.productoService.eliminarProducto(id).subscribe({
      next: () => {
        if (this.editandoId() === id) {
          this.cancelarEdicion();
        }
        this.aviso.set('Producto eliminado correctamente.');
        this.cargarProductos();
      },
      error: () => this.error.set('No se pudo eliminar el producto.'),
    });
  }

  guardarProducto(): void {
    this.guardando.set(true);
    this.aviso.set('');
    this.error.set('');

    const id = this.editandoId();
    const peticion = id === null
      ? this.productoService.crearProducto(this.productoNuevo)
      : this.productoService.actualizarProducto(id, this.productoNuevo);

    peticion.subscribe({
      next: () => {
        this.productoNuevo = this.crearProductoVacio();
        this.editandoId.set(null);
        this.aviso.set(id === null ? 'Producto agregado correctamente.' : 'Producto actualizado correctamente.');
        this.guardando.set(false);
        this.cargarProductos();
      },
      error: () => {
        this.error.set(id === null ? 'No se pudo agregar el producto.' : 'No se pudo actualizar el producto.');
        this.guardando.set(false);
      },
    });
  }

  private crearProductoVacio(): ProductoRequest {
    return {
      nombre: '',
      descripcion: '',
      precio: null,
      categoriaId: null,
      imagenUrl: null,
      esDestacado: false,
      estaDisponible: true,
    };
  }
}
