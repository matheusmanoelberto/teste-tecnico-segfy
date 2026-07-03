export function onlyDigits(value: string) {
  return value.replace(/\D/g, '')
}

export function normalizeDocument(value: string) {
  return onlyDigits(value)
}
