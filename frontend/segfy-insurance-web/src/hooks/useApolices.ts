import { useCallback, useEffect, useMemo, useState } from 'react'
import { apoliceService } from '../services/apoliceService'
import type { Apolice } from '../types/Apolice.type'
import type { AtualizarApoliceRequest } from '../types/AtualizarApoliceRequest.type'
import type { CriarApoliceRequest } from '../types/CriarApoliceRequest.type'
import { validateApolice } from '../validations/apoliceSchema'

export function useApolices() {
  const [apolices, setApolices] = useState<Apolice[]>([])
  const [apolicesVencendo, setApolicesVencendo] = useState<Apolice[]>([])
  const [loading, setLoading] = useState(true)
  const [message, setMessage] = useState('')
  const [error, setError] = useState('')

  const load = useCallback(async () => {
    setLoading(true)
    setError('')

    try {
      const [allPolicies, expiringPolicies] = await Promise.all([
        apoliceService.listar(),
        apoliceService.listarVencendo(),
      ])

      setApolices(allPolicies)
      setApolicesVencendo(expiringPolicies)
    } catch (failure) {
      setError(failure instanceof Error ? failure.message : 'Erro ao carregar apolices.')
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    void load()
  }, [load])

  const create = useCallback(
    async (request: CriarApoliceRequest) => {
      setMessage('')
      setError('')

      const errors = validateApolice(request)
      if (errors.length > 0) {
        setError(errors.join(' '))
        return false
      }

      try {
        await apoliceService.criar(request)
        setMessage('Apolice cadastrada com sucesso.')
        await load()
        return true
      } catch (failure) {
        setError(failure instanceof Error ? failure.message : 'Erro ao cadastrar apolice.')
        return false
      }
    },
    [load],
  )

  const update = useCallback(
    async (id: string, request: AtualizarApoliceRequest) => {
      setMessage('')
      setError('')

      const errors = validateApolice(request)
      if (errors.length > 0) {
        setError(errors.join(' '))
        return false
      }

      try {
        await apoliceService.atualizar(id, request)
        setMessage('Apolice atualizada com sucesso.')
        await load()
        return true
      } catch (failure) {
        setError(failure instanceof Error ? failure.message : 'Erro ao atualizar apolice.')
        return false
      }
    },
    [load],
  )

  const cancel = useCallback(
    async (id: string) => {
      setMessage('')
      setError('')

      try {
        await apoliceService.cancelar(id)
        setMessage('Apolice cancelada com sucesso.')
        await load()
      } catch (failure) {
        setError(failure instanceof Error ? failure.message : 'Erro ao cancelar apolice.')
      }
    },
    [load],
  )

  const remove = useCallback(
    async (id: string) => {
      setMessage('')
      setError('')

      try {
        await apoliceService.remover(id)
        setMessage('Apolice removida com sucesso.')
        await load()
      } catch (failure) {
        setError(failure instanceof Error ? failure.message : 'Erro ao remover apolice.')
      }
    },
    [load],
  )

  const monthlyPremiumTotal = useMemo(
    () => apolices.reduce((total, apolice) => total + apolice.premioMensal, 0),
    [apolices],
  )

  return {
    apolices,
    apolicesVencendo,
    loading,
    message,
    error,
    monthlyPremiumTotal,
    create,
    update,
    cancel,
    remove,
    reload: load,
  }
}
