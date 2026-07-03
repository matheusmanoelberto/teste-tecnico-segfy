export function normalizePlate(value: string) {
  return value.replace(/[-\s]/g, '').toUpperCase()
}

export function isValidPlate(value: string) {
  const plate = normalizePlate(value)
  const oldPlate = /^[A-Z]{3}[0-9]{4}$/
  const mercosulPlate = /^[A-Z]{3}[0-9]{1}[A-Z]{1}[0-9]{2}$/

  return oldPlate.test(plate) || mercosulPlate.test(plate)
}
