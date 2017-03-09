import { number, object, string, date } from 'yup'
import { formatDateTime } from 'helpers/dateHelper'

const defaultDate = formatDateTime(new Date())
const baseSchema = {

  name: string()
    .ensure()
    .min(2, 'min 2 symbols')
    .max(100, 'max 100 symbols')
    .required('NameIsRequired'),

  dataSource: string().ensure(),
  shortName: string().ensure(),
  addressId: string().ensure(),
  liqReason: string().ensure(),
  liqDate: string().ensure(),
  registrationReason: string().ensure(),
  contactPerson: string().ensure(),
  classified: string().ensure(),
  foreignParticipation: string().ensure(),
  reorgTypeCode: string().ensure(),
  suspensionEnd: string().ensure(),
  suspensionStart: string().ensure(),
  telephoneNo: string().ensure(),
  emailAddress: string().ensure(),
  webAddress: string().ensure(),
  reorgReferences: string().ensure(),
  notes: string().ensure(),
  employeesDate: date().default(defaultDate),
  employeesYear: date().default(defaultDate),
  externalIdDate: date().default(defaultDate),
  registrationDate: date().default(defaultDate),
  reorgDate: date().default(defaultDate),
  statIdDate: date().default(defaultDate),
  statusDate: date().default(defaultDate),
  taxRegDate: date().default(defaultDate),
  turnoveDate: date().default(defaultDate),
  turnoverYear: date().default(defaultDate),
  endPeriod: date().default(defaultDate),
  startPeriod: date().default(defaultDate),
  regIdDate: date().default(defaultDate),
}

const localUnit = {
  legalUnitIdDate: date().default(defaultDate),
}

const legalUnit = {
  entRegIdDate: date().default(defaultDate),
  founders: string().ensure(),
  owner: string().ensure(),
  legalForm: string().ensure(),
  instSectorCode: string().ensure(),
  totalCapital: string().ensure(),
  munCapitalShare: string().ensure(),
  stateCapitalShare: string().ensure(),
  privCapitalShare: string().ensure(),
  foreignCapitalShare: string().ensure(),
  foreignCapitalCurrency: string().ensure(),
  actualMainActivity1: string().ensure(),
  actualMainActivity2: string().ensure(),
  actualMainActivityDate: string().ensure(),
  enterpriseRegId: number().required('EnterpriseIsRequired'),
}

const enterpriseUnit = {
  entGroupIdDate: date().default(defaultDate),
  instSectorCode: string().ensure(),
  totalCapital: string().ensure(),
  munCapitalShare: string().ensure(),
  stateCapitalShare: string().ensure(),
  privCapitalShare: string().ensure(),
  foreignCapitalShare: string().ensure(),
  foreignCapitalCurrency: string().ensure(),
  actualMainActivity1: string().ensure(),
  actualMainActivity2: string().ensure(),
  actualMainActivityDate: string().ensure(),
  entGroupRole: string().ensure(),
  entGroupId: number().required('EnterpriseGroupIsRequired'),
}

const enterpriseGroup = {
  statId: number(),
  statIdDate: date().default(defaultDate),
  taxRegId: number(),
  taxRegDate: date().default(defaultDate),
  externalId: number(),
  externalIdType: number(),
  externalIdDate: date().default(defaultDate),
  dataSource: string().ensure(),
  name: string()
    .ensure()
    .min(2, 'min 2 symbols')
    .max(100, 'max 100 symbols')
    .required('NameIsRequired'),
  shortName: string().ensure(),
  telephoneNo: string().ensure(),
  emailAddress: string().ensure(),
  wbAddress: string().ensure(),
  entGroupType: string().ensure(),
  registrationDate: date().default(defaultDate),
  registrationReason: string().ensure(),
  liqDateStart: date().default(defaultDate),
  liqDateEnd: date().default(defaultDate),
  liqReason: string().ensure(),
  suspensionStart: string().ensure(),
  suspensionEnd: string().ensure(),
  reorgTypeCode: string().ensure(),
  reorgDate: date().default(defaultDate),
  reorgReferences: string().ensure(),
  contactPerson: string().ensure(),
  employees: number(),
  employeesFte: number(),
  employeesYear: date().default(defaultDate),
  employeesDate: date().default(defaultDate),
  turnover: number(),
  turnoverYear: date().default(defaultDate),
  turnoveDate: date().default(defaultDate),
  status: string().ensure(),
  statusDate: date().default(defaultDate),
  notes: string().ensure(),
}

export const getSchema = (statUnitType) => {
  let statUnitSchema
  let temp
  switch (statUnitType) {
    case 1:
      temp = { ...baseSchema, ...localUnit }
      statUnitSchema = object(baseSchema)
      break
    case 2:
      temp = { ...baseSchema, ...legalUnit }
      statUnitSchema = object(temp)
      break
    case 3:
      temp = { ...baseSchema, ...enterpriseUnit }
      statUnitSchema = object(temp)
      break
    case 4:
      temp = { ...baseSchema, ...enterpriseGroup }
      statUnitSchema = object(temp)
      break
    default:
      statUnitSchema = object(baseSchema)
      break
  }
  return statUnitSchema
}

export default { getSchema }

