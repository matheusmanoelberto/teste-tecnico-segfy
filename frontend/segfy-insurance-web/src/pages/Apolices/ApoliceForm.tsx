import { useState } from 'react'
import { Button } from '../../components/ui/Button'
import { Input } from '../../components/ui/Input'
import type { Apolice } from '../../types/Apolice.type'
import type { CriarApoliceRequest } from '../../types/CriarApoliceRequest.type'

type ApoliceFormProps = {
  initialData?: Apolice
  submitLabel: string
  onSubmit: (data: CriarApoliceRequest) => Promise<boolean>
}

const emptyForm: CriarApoliceRequest = {
  documentoSegurado: '',
  placaVeiculo: '',
  premioMensal: 0,
  dataInicio: '',
  dataFim: '',
}

export function ApoliceForm({ initialData, submitLabel, onSubmit }: ApoliceFormProps) {
  const [form, setForm] = useState<CriarApoliceRequest>(
    initialData
      ? {
          documentoSegurado: initialData.documentoSegurado,
          placaVeiculo: initialData.placaVeiculo,
          premioMensal: initialData.premioMensal,
          dataInicio: initialData.dataInicio,
          dataFim: initialData.dataFim,
        }
      : emptyForm,
  )
  const [submitting, setSubmitting] = useState(false)

  async function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault()
    setSubmitting(true)

    const success = await onSubmit(form)
    if (success && !initialData) {
      setForm(emptyForm)
    }

    setSubmitting(false)
  }

  return (
    <form className="policy-form" onSubmit={handleSubmit}>
      <Input
        id="documentoSegurado"
        label="CPF/CNPJ do segurado"
        required
        value={form.documentoSegurado}
        onChange={(event) => setForm((current) => ({ ...current, documentoSegurado: event.target.value }))}
      />
      <Input
        id="placaVeiculo"
        label="Placa do veiculo"
        required
        value={form.placaVeiculo}
        onChange={(event) => setForm((current) => ({ ...current, placaVeiculo: event.target.value }))}
      />
      <Input
        id="premioMensal"
        label="Premio mensal"
        min="0.01"
        required
        step="0.01"
        type="number"
        value={form.premioMensal || ''}
        onChange={(event) => setForm((current) => ({ ...current, premioMensal: Number(event.target.value) }))}
      />
      <Input
        id="dataInicio"
        label="Inicio da vigencia"
        required
        type="date"
        value={form.dataInicio}
        onChange={(event) => setForm((current) => ({ ...current, dataInicio: event.target.value }))}
      />
      <Input
        id="dataFim"
        label="Fim da vigencia"
        required
        type="date"
        value={form.dataFim}
        onChange={(event) => setForm((current) => ({ ...current, dataFim: event.target.value }))}
      />
      <Button disabled={submitting} type="submit">
        {submitting ? 'Salvando...' : submitLabel}
      </Button>
    </form>
  )
}
