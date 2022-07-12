import dayjs from 'dayjs'
import duration from 'dayjs/plugin/duration'
import localized from 'dayjs/plugin/localizedFormat'
import relative from 'dayjs/plugin/relativeTime'

dayjs.extend(duration)
dayjs.extend(localized)
dayjs.extend(relative)
