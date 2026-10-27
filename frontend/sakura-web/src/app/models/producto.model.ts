export interface Producto {
  id: number;
  nombre: string;
  descripcion: string;
  precio: number | null;
  categoriaId: number | null;
  nombreCategoria: string | null;
  imagenUrl: string | null;
  esDestacado: boolean;
  estaDisponible: boolean;
}

export type ProductoRequest = Omit<Producto, 'id' | 'nombreCategoria'>;
