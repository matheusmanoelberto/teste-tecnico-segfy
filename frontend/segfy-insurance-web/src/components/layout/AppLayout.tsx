import type { ReactNode } from 'react'
import type { Tela } from '../../routes/AppRoutes'
import { Header } from './Header'
import { Sidebar } from './Sidebar'

type AppLayoutProps = {
  telaAtual: Tela
  onNavigate: (tela: Tela) => void
  children: ReactNode
}

export function AppLayout({ telaAtual, onNavigate, children }: AppLayoutProps) {
  return (
    <div className="app-shell">
      <Header />
      <div className="app-body">
        <Sidebar telaAtual={telaAtual} onNavigate={onNavigate} />
        {children}
      </div>
    </div>
  )
}
