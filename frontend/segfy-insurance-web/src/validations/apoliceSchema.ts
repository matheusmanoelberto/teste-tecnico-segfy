import type { CriarApoliceRequest } from '../types/CriarApoliceRequest.type'
import { isValidPlate } from '../utils/plate'

export function validateApolice(data: CriarApoliceRequest) {
  const errors: string[] = []
  const document = data.documentoSegurado.replace(/\D/g, '')

  if (document.length !== 11 && document.length !== 14) {
    errors.push('Documento deve ter 11 ou 14 digitos.')
  }

  if (!isValidPlate(data.placaVeiculo)) {
    errors.push('Placa deve estar no padrao ABC1234 ou ABC1D23.')
  }

  if (data.premioMensal <= 0) {
    errors.push('Premio mensal deve ser maior que zero.')
  }

  if (!data.dataInicio || !data.dataFim || data.dataFim < data.dataInicio) {
    errors.push('Vigencia deve possuir data final maior ou igual a inicial.')
  }

  return errors
}
