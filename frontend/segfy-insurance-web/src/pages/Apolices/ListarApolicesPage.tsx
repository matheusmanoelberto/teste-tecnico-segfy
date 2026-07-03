import { Badge } from '../../components/ui/Badge'
import { Button } from '../../components/ui/Button'
import { Card } from '../../components/ui/Card'
import { Loading } from '../../components/ui/Loading'
import { Table } from '../../components/ui/Table'
import type { Tela } from '../../routes/AppRoutes'
import type { Apolice } from '../../types/Apolice.type'
import { formatCurrency } from '../../utils/currency'
import { formatDate } from '../../utils/date'

type ListarApolicesPageProps = {
  apolices: Apolice[]
  loading: boolean
  onCancel: (id: string) => Promise<void>
  onRemove: (id: string) => Promise<void>
  onEdit: (apolice: Apolice) => void
  onNavigate: (tela: Tela) => void
}

function statusTone(status: string) {
  if (status === 'Ativa') return 'success'
  if (status === 'Cancelada') return 'danger'
  if (status === 'Expirada') return 'warning'
  return 'neutral'
}

export function ListarApolicesPage({
  apolices,
  loading,
  onCancel,
  onRemove,
  onEdit,
  onNavigate,
}: ListarApolicesPageProps) {
  if (loading) {
    return <Loading text="Carregando apolices..." />
  }

  return (
    <Card>
      <div className="page-actions">
        <h2>Apolices cadastradas</h2>
        <Button onClick={() => onNavigate('criar')}>Nova apolice</Button>
      </div>
      {apolices.length === 0 ? (
        <p className="support-text">Nenhuma apolice cadastrada.</p>
      ) : (
        <Table>
          <thead>
            <tr>
              <th>Numero</th>
              <th>Documento</th>
              <th>Placa</th>
              <th>Premio</th>
              <th>Vigencia</th>
              <th>Status</th>
              <th>Acoes</th>
            </tr>
          </thead>
          <tbody>
            {apolices.map((apolice) => (
              <tr key={apolice.id}>
                <td>{apolice.numeroApolice}</td>
                <td>{apolice.documentoSegurado}</td>
                <td>{apolice.placaVeiculo}</td>
                <td>{formatCurrency(apolice.premioMensal)}</td>
                <td>
                  {formatDate(apolice.dataInicio)} ate {formatDate(apolice.dataFim)}
                </td>
                <td>
                  <Badge tone={statusTone(apolice.situacao)}>{apolice.situacao}</Badge>
                </td>
                <td className="actions">
                  <Button onClick={() => onEdit(apolice)} variant="secondary">
                    Editar
                  </Button>
                  <Button
                    disabled={apolice.situacao !== 'Ativa'}
                    onClick={() => onCancel(apolice.id)}
                    variant="secondary"
                  >
                    Cancelar
                  </Button>
                  <Button onClick={() => onRemove(apolice.id)} variant="danger">
                    Remover
                  </Button>
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      )}
    </Card>
  )
}
