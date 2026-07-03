import { useState } from 'react'
import { AppLayout } from '../components/layout/AppLayout'
import { PageContainer } from '../components/layout/PageContainer'
import { Card } from '../components/ui/Card'
import { CriarApolicePage } from '../pages/Apolices/CriarApolicePage'
import { EditarApolicePage } from '../pages/Apolices/EditarApolicePage'
import { ListarApolicesPage } from '../pages/Apolices/ListarApolicesPage'
import { ApolicesVencendoPage } from '../pages/Apolices/ApolicesVencendoPage'
import { useApolices } from '../hooks/useApolices'
import type { Apolice } from '../types/Apolice.type'
import { formatCurrency } from '../utils/currency'

export type Tela = 'listar' | 'criar' | 'editar' | 'vencendo'

export function AppRoutes() {
  const [currentScreen, setCurrentScreen] = useState<Tela>('listar')
  const [selectedPolicy, setSelectedPolicy] = useState<Apolice | null>(null)
  const {
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
  } = useApolices()

  function handleEdit(apolice: Apolice) {
    setSelectedPolicy(apolice)
    setCurrentScreen('editar')
  }

  return (
    <AppLayout telaAtual={currentScreen} onNavigate={setCurrentScreen}>
      <PageContainer title="Apolices">
        <section className="summary-grid">
          <Card>
            <span className="summary-label">Apolices</span>
            <strong>{apolices.length}</strong>
          </Card>
          <Card>
            <span className="summary-label">Vencem em 30 dias</span>
            <strong>{apolicesVencendo.length}</strong>
          </Card>
          <Card>
            <span className="summary-label">Premios mensais</span>
            <strong>{formatCurrency(monthlyPremiumTotal)}</strong>
          </Card>
        </section>

        {message && <div className="message message-success">{message}</div>}
        {error && <div className="message message-error">{error}</div>}

        {currentScreen === 'listar' && (
          <ListarApolicesPage
            apolices={apolices}
            loading={loading}
            onCancel={cancel}
            onEdit={handleEdit}
            onNavigate={setCurrentScreen}
            onRemove={remove}
          />
        )}
        {currentScreen === 'criar' && <CriarApolicePage onCreate={create} />}
        {currentScreen === 'editar' && (
          <EditarApolicePage selectedPolicy={selectedPolicy} onUpdate={update} />
        )}
        {currentScreen === 'vencendo' && (
          <ApolicesVencendoPage apolices={apolicesVencendo} loading={loading} />
        )}
      </PageContainer>
    </AppLayout>
  )
}
