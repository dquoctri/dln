import React, { Fragment } from 'react'
import { useTheme } from '@mui/material/styles'
import { AppBar, Box, Fab, Toolbar } from '@mui/material'
import useScrollTrigger from '@mui/material/useScrollTrigger'
import { KeyboardArrowUp } from '@mui/icons-material'
import LogoSection from './LogoSection'
import ScrollTop from '../../common/ScrollTop'

const Header = () => {
  const theme = useTheme()
  const trigger = useScrollTrigger({
    disableHysteresis: true,
    threshold: 0,
  })
  return (
    <Fragment>
      <AppBar
        enableColorOnDark
        elevation={trigger ? 4 : 0}
        sx={{
          bgcolor: theme.palette.background.default,
        }}
      >
        <Toolbar>
          <Box component="span" sx={{ display: { xs: 'none', sm: 'block' }, flexGrow: 1 }}>
            <LogoSection />
          </Box>
          {/* todo */}
        </Toolbar>
      </AppBar>
      <Toolbar id="back-to-top-anchor" />
      <ScrollTop anchorId="back-to-top-anchor">
        <Fab
          color="secondary"
          style={{ borderRadius: '5%' }}
          size="small"
          variant="circular"
          aria-label="scroll back to top"
        >
          <KeyboardArrowUp />
        </Fab>
      </ScrollTop>
    </Fragment>
  )
}

export default Header
