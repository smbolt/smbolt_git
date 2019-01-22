<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns="http://www.oopfactory.com/2011/XSL/Hipaa">
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/" >
    <xsl:apply-templates select "@* | node()"/>
  <xsl:template>


  <xsl:template match="@* | node()">
    <xsl:apply-templates select="@* | node()"/>
  </xsl:template>


  <xsl:template match="ClaimDocument">
    <Interchange>
      <xsl:apply-templates select="@* | node()"/>
    </Interchange>
  </xsl:template>


</xsl:stylesheet>
