type BadgeProps = {
  children: string
  tone?: 'success' | 'danger' | 'warning' | 'neutral'
}

export function Badge({ children, tone = 'neutral' }: BadgeProps) {
  return <span className={`badge badge-${tone}`}>{children}</span>
}
