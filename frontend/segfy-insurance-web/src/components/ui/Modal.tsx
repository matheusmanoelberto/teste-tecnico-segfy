import type { ReactNode } from 'react'
import { Button } from './Button'

type ModalProps = {
  title: string
  open: boolean
  onClose: () => void
  children: ReactNode
}

export function Modal({ title, open, onClose, children }: ModalProps) {
  if (!open) {
    return null
  }

  return (
    <div className="modal-backdrop" role="presentation">
      <div className="modal" role="dialog" aria-modal="true" aria-label={title}>
        <header className="modal-header">
          <h2>{title}</h2>
          <Button onClick={onClose} variant="secondary">
            Fechar
          </Button>
        </header>
        {children}
      </div>
    </div>
  )
}
