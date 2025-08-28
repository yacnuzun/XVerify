<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                xmlns:sch="http://purl.oclc.org/dsdl/schematron"
                xmlns:svrl="http://purl.oclc.org/dsdl/svrl"
                version="1.0">

  <xsl:output method="xml" indent="yes"/>
  
  <!-- Root template -->
  <xsl:template match="/">
    <svrl:schematron-output>
      <xsl:apply-templates select="*"/>
    </svrl:schematron-output>
  </xsl:template>

  <!-- Rule: Invoice must have TotalAmount > 0 -->
  <xsl:template match="Invoice">
    <xsl:choose>
      <xsl:when test="TotalAmount &gt; 0">
        <svrl:successful-report test="TotalAmount &gt; 0"
                                location="Invoice">
          Toplam tutar 0'dan büyük.
        </svrl:successful-report>
      </xsl:when>
      <xsl:otherwise>
        <svrl:failed-assert test="TotalAmount &gt; 0"
                            location="Invoice">
          <svrl:text>Toplam tutar 0'dan büyük olmalı.</svrl:text>
        </svrl:failed-assert>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <!-- Identity template (varsayılan olarak alt node’lara inmeyi sağlar) -->
  <xsl:template match="*">
    <xsl:apply-templates select="*"/>
  </xsl:template>

</xsl:stylesheet>
