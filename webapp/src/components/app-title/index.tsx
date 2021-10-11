import React from "react";
import { Helmet } from "react-helmet-async";
import { useTranslation } from 'react-i18next'
import { LocalizationKeys } from '../../locales'

// define interface to represent component props
interface Props {
  title: string
}

const AppTitle = ({ title }: Props) => {
  const { t } = useTranslation()
  return (
    <Helmet defaultTitle={t(LocalizationKeys.title)}>
      <title>{title}</title>
    </Helmet>
  );
};

export default AppTitle;