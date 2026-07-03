type LoadingProps = {
  text?: string
}

export function Loading({ text = 'Carregando...' }: LoadingProps) {
  return <p className="support-text">{text}</p>
}
