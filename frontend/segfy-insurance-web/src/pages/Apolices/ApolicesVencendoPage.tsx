import { Card } from '../../components/ui/Card'
import { Loading } from '../../components/ui/Loading'
import { Table } from '../../components/ui/Table'
import type { Apolice } from '../../types/Apolice.type'
import { formatDate } from '../../utils/date'

type ApolicesVencendoPageProps = {
  apolices: Apolice[]
  loading: boolean
}

export function ApolicesVencendoPage({ apolices, loading }: ApolicesVencendoPageProps) {
  if (loading) {
    return <Loading text="Carregando vencimentos..." />
  }

  return (
    <Card title="Apolices vencendo nos proximos 30 dias">
      {apolices.length === 0 ? (
        <p className="support-text">Nenhuma apolice ativa vence nos proximos 30 dias.</p>
      ) : (
        <Table>
          <thead>
            <tr>
              <th>Numero</th>
              <th>Placa</th>
              <th>Documento</th>
              <th>Vencimento</th>
            </tr>
          </thead>
          <tbody>
            {apolices.map((apolice) => (
              <tr key={apolice.id}>
                <td>{apolice.numeroApolice}</td>
                <td>{apolice.placaVeiculo}</td>
                <td>{apolice.documentoSegurado}</td>
                <td>{formatDate(apolice.dataFim)}</td>
              </tr>
            ))}
          </tbody>
        </Table>
      )}
    </Card>
  )
}
