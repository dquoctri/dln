import React from 'react'

import { ButtonBase } from '@mui/material'
import { LogoDevSharp } from '@mui/icons-material'
import { red } from '@mui/material/colors'

const LogoSection = () => (
  <ButtonBase disableRipple>
    <LogoDevSharp sx={{ color: red[500] }} />
  </ButtonBase>
);

export default LogoSection;