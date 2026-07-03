export function formatDate(value: string) {
  if (!value) {
    return ''
  }

  return new Intl.DateTimeFormat('pt-BR', { timeZone: 'UTC' }).format(new Date(value))
}
