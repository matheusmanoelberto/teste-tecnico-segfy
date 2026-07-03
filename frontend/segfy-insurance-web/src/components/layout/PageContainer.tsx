import type { ReactNode } from 'react'

type PageContainerProps = {
  title: string
  children: ReactNode
}

export function PageContainer({ title, children }: PageContainerProps) {
  return (
    <main className="page-container">
      <h2>{title}</h2>
      {children}
    </main>
  )
}
