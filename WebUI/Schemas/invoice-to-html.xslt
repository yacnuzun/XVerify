<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <title>Invoice</title>
        <style>
          body { font-family: Arial; margin: 20px; }
          table { border-collapse: collapse; width: 50%; }
          td, th { border: 1px solid #ccc; padding: 8px; }
        </style>
      </head>
      <body>
        <h2>Invoice</h2>
        <table>
          <tr><th>Invoice Number</th><td><xsl:value-of select="Invoice/InvoiceNumber"/></td></tr>
          <tr><th>Issue Date</th><td><xsl:value-of select="Invoice/IssueDate"/></td></tr>
          <tr><th>Customer</th><td><xsl:value-of select="Invoice/CustomerName"/></td></tr>
          <tr><th>Total Amount</th><td><xsl:value-of select="Invoice/TotalAmount"/></td></tr>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
