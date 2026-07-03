import { Card } from '../../components/ui/Card'
import type { Apolice } from '../../types/Apolice.type'
import type { AtualizarApoliceRequest } from '../../types/AtualizarApoliceRequest.type'
import { ApoliceForm } from './ApoliceForm'

type EditarApolicePageProps = {
  selectedPolicy: Apolice | null
  onUpdate: (id: string, data: AtualizarApoliceRequest) => Promise<boolean>
}

export function EditarApolicePage({ selectedPolicy, onUpdate }: EditarApolicePageProps) {
  if (!selectedPolicy) {
    return (
      <Card title="Editar apolice">
        <p className="support-text">Selecione uma apolice na listagem para editar.</p>
      </Card>
    )
  }

  return (
    <Card title={`Editar ${selectedPolicy.numeroApolice}`}>
      <ApoliceForm
        initialData={selectedPolicy}
        onSubmit={(data) => onUpdate(selectedPolicy.id, data)}
        submitLabel="Atualizar apolice"
      />
    </Card>
  )
}
