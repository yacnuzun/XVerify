<?xml version="1.0" encoding="UTF-8"?>
<schema xmlns="http://purl.oclc.org/dsdl/schematron">
  <pattern id="business-rules">
    <rule context="Invoice">
      <assert test="TotalAmount &gt; 0">
        Toplam tutar 0'dan büyük olmalı.
      </assert>
    </rule>
  </pattern>
</schema>
