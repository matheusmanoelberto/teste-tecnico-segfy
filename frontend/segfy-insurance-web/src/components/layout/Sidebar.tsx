import type { Tela } from '../../routes/AppRoutes'

type SidebarProps = {
  telaAtual: Tela
  onNavigate: (tela: Tela) => void
}

const itens: Array<{ label: string; tela: Tela }> = [
  { label: 'Listar apolices', tela: 'listar' },
  { label: 'Criar apolice', tela: 'criar' },
  { label: 'Editar apolice', tela: 'editar' },
  { label: 'Vencimentos', tela: 'vencendo' },
]

export function Sidebar({ telaAtual, onNavigate }: SidebarProps) {
  return (
    <aside className="sidebar">
      <strong>Menu</strong>
      <nav>
        {itens.map((item) => (
          <button
            className={item.tela === telaAtual ? 'nav-item nav-item-active' : 'nav-item'}
            key={item.tela}
            onClick={() => onNavigate(item.tela)}
            type="button"
          >
            {item.label}
          </button>
        ))}
      </nav>
    </aside>
  )
}
