
import { WebTracerProvider } from '@opentelemetry/sdk-trace-web';
import { ConsoleSpanExporter, SimpleSpanProcessor} from '@opentelemetry/sdk-trace-base';
import { ZoneContextManager } from '@opentelemetry/context-zone';
import { registerInstrumentations } from '@opentelemetry/instrumentation';
import { FetchInstrumentation } from '@opentelemetry/instrumentation-fetch';
import { XMLHttpRequestInstrumentation } from '@opentelemetry/instrumentation-xml-http-request';
import { DocumentLoadInstrumentation } from '@opentelemetry/instrumentation-document-load';




// Create the exporter

// Create the provider with span processors
const provider = new WebTracerProvider({
 spanProcessors: [new SimpleSpanProcessor(new ConsoleSpanExporter())],
});

provider.getTracer("angular","v1.0.0",{
    schemaUrl:"http://localhost:4318/v1/traces"
})

provider.register({
 contextManager: new ZoneContextManager(),
});

registerInstrumentations({
 instrumentations: [
 new DocumentLoadInstrumentation(),
 new FetchInstrumentation(),
 new XMLHttpRequestInstrumentation(), ],
});
