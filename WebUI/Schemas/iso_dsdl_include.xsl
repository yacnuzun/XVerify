<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  version="2.0">

  <xsl:output method="xml" indent="yes"/>

  <xsl:param name="document-uri" select="document-uri(.)"/>

  <xsl:template match="/">
    <xsl:apply-templates select="." mode="include"/>
  </xsl:template>

  <xsl:template match="*|@*|text()|comment()|processing-instruction()" mode="include">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()" mode="include"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="sch:include" mode="include"
    xmlns:sch="http://purl.oclc.org/dsdl/schematron">
    <xsl:variable name="href" select="@href"/>
    <xsl:apply-templates select="document($href, /)" mode="include"/>
  </xsl:template>

</xsl:stylesheet>
