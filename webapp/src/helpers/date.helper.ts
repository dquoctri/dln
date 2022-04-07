export const formatStringToLocale = (utcStrDate?: string, defaultStr = ''): string => {
  if (!utcStrDate) {
    return defaultStr
  }

  return new Date(utcStrDate)?.toLocaleDateString() ?? defaultStr
}
