import type { Apolice } from '../types/Apolice.type'
import type { AtualizarApoliceRequest } from '../types/AtualizarApoliceRequest.type'
import type { CriarApoliceRequest } from '../types/CriarApoliceRequest.type'
import { api } from './api'

const route = '/api/apolices-seguro'

export const apoliceService = {
  listar() {
    return api<Apolice[]>(route)
  },

  listarVencendo() {
    return api<Apolice[]>(`${route}/vencendo-proximos-30-dias`)
  },

  criar(request: CriarApoliceRequest) {
    return api<Apolice>(route, {
      method: 'POST',
      body: JSON.stringify(request),
    })
  },

  atualizar(id: string, request: AtualizarApoliceRequest) {
    return api<void>(`${route}/${id}`, {
      method: 'PUT',
      body: JSON.stringify(request),
      noContent: true,
    })
  },

  cancelar(id: string) {
    return api<void>(`${route}/${id}/cancelar`, {
      method: 'PATCH',
      noContent: true,
    })
  },

  remover(id: string) {
    return api<void>(`${route}/${id}`, {
      method: 'DELETE',
      noContent: true,
    })
  },
}
