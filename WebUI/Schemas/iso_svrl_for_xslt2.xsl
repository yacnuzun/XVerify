<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:sch="http://purl.oclc.org/dsdl/schematron"
  xmlns:svrl="http://purl.oclc.org/dsdl/svrl"
  version="2.0">

  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/">
    <svrl:schematron-output>
      <xsl:apply-templates select="//sch:rule"/>
    </svrl:schematron-output>
  </xsl:template>

  <xsl:template match="sch:rule">
    <xsl:variable name="context" select="@context"/>
    <xsl:for-each select="//*[name()=$context]">
      <xsl:apply-templates select="current()/sch:assert"/>
    </xsl:for-each>
  </xsl:template>

  <xsl:template match="sch:assert">
    <xsl:if test="not(current()/@test) or not(boolean(current()/../..//sch:rule/@context))">
      <svrl:failed-assert test="{@test}">
        <xsl:value-of select="normalize-space(.)"/>
      </svrl:failed-assert>
    </xsl:if>
  </xsl:template>

</xsl:stylesheet>
