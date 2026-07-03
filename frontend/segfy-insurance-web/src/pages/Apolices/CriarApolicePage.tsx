import { Card } from '../../components/ui/Card'
import type { CriarApoliceRequest } from '../../types/CriarApoliceRequest.type'
import { ApoliceForm } from './ApoliceForm'

type CriarApolicePageProps = {
  onCreate: (data: CriarApoliceRequest) => Promise<boolean>
}

export function CriarApolicePage({ onCreate }: CriarApolicePageProps) {
  return (
    <Card title="Nova apolice">
      <ApoliceForm onSubmit={onCreate} submitLabel="Cadastrar apolice" />
    </Card>
  )
}
